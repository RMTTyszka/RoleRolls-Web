import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnDestroy,
  PLATFORM_ID,
  ViewChild,
  booleanAttribute,
  forwardRef,
  inject
} from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import type * as CodeMirrorCommandsModule from '@codemirror/commands';
import type * as CodeMirrorLanguageModule from '@codemirror/language';
import type * as CodeMirrorMarkdownModule from '@codemirror/lang-markdown';
import type * as CodeMirrorStateModule from '@codemirror/state';
import type * as CodeMirrorViewModule from '@codemirror/view';
import { MarkdownViewerComponent } from '@app/shared/components/markdown-viewer/markdown-viewer.component';

type CodeMirrorRuntime = {
  commands: typeof CodeMirrorCommandsModule;
  language: typeof CodeMirrorLanguageModule;
  markdown: typeof CodeMirrorMarkdownModule;
  state: typeof CodeMirrorStateModule;
  view: typeof CodeMirrorViewModule;
};

@Component({
  selector: 'rr-markdown-editor',
  imports: [MarkdownViewerComponent],
  templateUrl: './markdown-editor.component.html',
  styleUrl: './markdown-editor.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => MarkdownEditorComponent),
      multi: true
    }
  ]
})
export class MarkdownEditorComponent implements ControlValueAccessor, AfterViewInit, OnDestroy {
  @ViewChild('editorHost') private editorHost?: ElementRef<HTMLDivElement>;

  @Input() public placeholderText = 'Write in Markdown';
  @Input() public minHeight = '320px';
  @Input({ transform: booleanAttribute }) public showPreview = false;

  public value = '';
  public disabled = false;
  public editorReady = false;

  private readonly platformId = inject(PLATFORM_ID);
  private runtime?: CodeMirrorRuntime;
  private editableCompartment?: CodeMirrorStateModule.Compartment;
  private placeholderCompartment?: CodeMirrorStateModule.Compartment;
  private editorView?: CodeMirrorViewModule.EditorView;
  private editorLoadPromise?: Promise<void>;
  private destroyed = false;
  private onChange: (value: string) => void = () => undefined;
  private onTouched: () => void = () => undefined;

  public ngAfterViewInit(): void {
    void this.ensureEditor();
  }

  public ngOnDestroy(): void {
    this.destroyed = true;
    this.editorView?.destroy();
  }

  public writeValue(value: string | null | undefined): void {
    const nextValue = value ?? '';
    this.value = nextValue;

    if (!this.editorView) {
      return;
    }

    const currentValue = this.editorView.state.doc.toString();
    if (currentValue === nextValue) {
      return;
    }

    this.editorView.dispatch({
      changes: {
        from: 0,
        to: currentValue.length,
        insert: nextValue
      }
    });
  }

  public registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;

    if (!this.editorView || !this.editableCompartment || !this.runtime) {
      return;
    }

    this.editorView.dispatch({
      effects: this.editableCompartment.reconfigure(this.runtime.view.EditorView.editable.of(!isDisabled))
    });
  }

  public applyHeading(level = 1): void {
    this.prefixSelectedLines(`${'#'.repeat(level)} `);
  }

  public applyBold(): void {
    this.wrapSelection('**', '**', 'bold text');
  }

  public applyItalic(): void {
    this.wrapSelection('*', '*', 'italic text');
  }

  public applyLink(): void {
    const view = this.editorView;
    if (!view) {
      return;
    }

    const range = view.state.selection.main;
    const selectedText = view.state.sliceDoc(range.from, range.to) || 'link text';
    const replacement = `[${selectedText}](https://example.com)`;
    const urlStart = replacement.indexOf('https://');
    const from = range.from;

    view.dispatch({
      changes: { from, to: range.to, insert: replacement },
      selection: { anchor: from + urlStart, head: from + replacement.length - 1 },
      scrollIntoView: true
    });
    view.focus();
  }

  public applyBulletList(): void {
    this.prefixSelectedLines('- ');
  }

  public applyOrderedList(): void {
    this.prefixSelectedLines('', true);
  }

  public applyQuote(): void {
    this.prefixSelectedLines('> ');
  }

  public applyCodeBlock(): void {
    this.wrapSelection('```\n', '\n```', 'code');
  }

  private wrapSelection(prefix: string, suffix: string, placeholderText: string): void {
    const view = this.editorView;
    if (!view) {
      return;
    }

    const range = view.state.selection.main;
    const selectedText = view.state.sliceDoc(range.from, range.to);
    const text = selectedText || placeholderText;
    const replacement = `${prefix}${text}${suffix}`;
    const from = range.from;
    const selectionStart = from + prefix.length;
    const selectionEnd = selectionStart + text.length;

    view.dispatch({
      changes: { from, to: range.to, insert: replacement },
      selection: { anchor: selectionStart, head: selectionEnd },
      scrollIntoView: true
    });
    view.focus();
  }

  private prefixSelectedLines(prefix: string, ordered = false): void {
    const view = this.editorView;
    if (!view) {
      return;
    }

    const range = view.state.selection.main;
    const startLine = view.state.doc.lineAt(range.from);
    const endLine = view.state.doc.lineAt(range.to);
    const lines: string[] = [];

    for (let lineNumber = startLine.number; lineNumber <= endLine.number; lineNumber++) {
      const line = view.state.doc.line(lineNumber);
      const cleanedLine = ordered ? line.text.replace(/^\d+\.\s+/, '') : line.text;
      lines.push(ordered ? `${lineNumber - startLine.number + 1}. ${cleanedLine}` : `${prefix}${line.text}`);
    }

    const replacement = lines.join('\n');
    view.dispatch({
      changes: { from: startLine.from, to: endLine.to, insert: replacement },
      selection: { anchor: startLine.from, head: startLine.from + replacement.length },
      scrollIntoView: true
    });
    view.focus();
  }

  private async ensureEditor(): Promise<void> {
    if (!isPlatformBrowser(this.platformId) || !this.editorHost || this.editorView) {
      return;
    }

    if (this.editorLoadPromise) {
      return this.editorLoadPromise;
    }

    this.editorLoadPromise = this.loadCodeMirror().then((runtime) => {
      if (this.destroyed || !this.editorHost || this.editorView) {
        return;
      }

      this.runtime = runtime;
      this.createEditor(runtime);
      this.editorReady = true;
    }).finally(() => {
      this.editorLoadPromise = undefined;
    });

    return this.editorLoadPromise;
  }

  private createEditor(runtime: CodeMirrorRuntime): void {
    const { commands, language, markdown, state, view } = runtime;

    this.editableCompartment = new state.Compartment();
    this.placeholderCompartment = new state.Compartment();

    this.editorView = new view.EditorView({
      state: state.EditorState.create({
        doc: this.value,
        extensions: [
          view.lineNumbers(),
          view.highlightActiveLineGutter(),
          view.highlightActiveLine(),
          view.drawSelection(),
          commands.history(),
          markdown.markdown(),
          language.syntaxHighlighting(language.defaultHighlightStyle, { fallback: true }),
          view.EditorView.lineWrapping,
          view.keymap.of([
            commands.indentWithTab,
            ...commands.defaultKeymap,
            ...commands.historyKeymap
          ]),
          this.editableCompartment.of(view.EditorView.editable.of(!this.disabled)),
          this.placeholderCompartment.of(view.placeholder(this.placeholderText)),
          view.EditorView.updateListener.of((update) => {
            if (!update.docChanged) {
              return;
            }

            this.value = update.state.doc.toString();
            this.onChange(this.value);
          }),
          view.EditorView.domEventHandlers({
            blur: () => {
              this.onTouched();
            }
          }),
          view.EditorView.theme({
            '&': {
              border: '1px solid var(--rr-markdown-editor-border-color)',
              borderRadius: 'var(--rr-markdown-editor-radius)',
              backgroundColor: 'var(--rr-markdown-editor-background)',
              overflow: 'hidden'
            },
            '&.cm-focused': {
              outline: 'var(--rr-markdown-editor-focus-outline-width) solid var(--rr-markdown-editor-focus-outline-color)',
              outlineOffset: 'var(--rr-markdown-editor-focus-outline-offset)'
            },
            '.cm-gutters': {
              backgroundColor: 'var(--rr-markdown-editor-gutter-background)',
              color: 'var(--rr-markdown-editor-gutter-color)',
              borderRight: '1px solid var(--rr-markdown-editor-border-color)'
            },
            '.cm-scroller': {
              minHeight: 'var(--rr-markdown-editor-min-height, 320px)',
              fontFamily: 'var(--rr-markdown-editor-font-family)'
            },
            '.cm-content': {
              minHeight: 'var(--rr-markdown-editor-min-height, 320px)',
              padding: 'var(--rr-markdown-editor-content-padding)',
              caretColor: 'var(--rr-markdown-editor-caret-color)',
              color: 'var(--rr-markdown-editor-text-color)'
            },
            '.cm-line': {
              padding: '0'
            },
            '.cm-activeLine': {
              backgroundColor: 'var(--rr-markdown-editor-active-line-background)'
            },
            '.cm-activeLineGutter': {
              backgroundColor: 'var(--rr-markdown-editor-active-gutter-background)'
            },
            '.cm-selectionBackground, &.cm-focused .cm-selectionBackground': {
              backgroundColor: 'var(--rr-markdown-editor-selection-background)'
            }
          })
        ]
      }),
      parent: this.editorHost.nativeElement
    });
  }

  private async loadCodeMirror(): Promise<CodeMirrorRuntime> {
    const [commands, language, markdown, state, view] = await Promise.all([
      import('@codemirror/commands'),
      import('@codemirror/language'),
      import('@codemirror/lang-markdown'),
      import('@codemirror/state'),
      import('@codemirror/view')
    ]);

    return { commands, language, markdown, state, view };
  }
}
