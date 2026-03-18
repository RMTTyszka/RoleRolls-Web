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
import { Compartment, EditorSelection, EditorState } from '@codemirror/state';
import { defaultKeymap, history, historyKeymap, indentWithTab } from '@codemirror/commands';
import { markdown } from '@codemirror/lang-markdown';
import { defaultHighlightStyle, syntaxHighlighting } from '@codemirror/language';
import {
  EditorView,
  drawSelection,
  highlightActiveLine,
  highlightActiveLineGutter,
  keymap,
  lineNumbers,
  placeholder
} from '@codemirror/view';
import { MarkdownViewerComponent } from '@app/shared/components/markdown-viewer/markdown-viewer.component';

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

  private readonly platformId = inject(PLATFORM_ID);
  private readonly editableCompartment = new Compartment();
  private readonly placeholderCompartment = new Compartment();
  private editorView?: EditorView;
  private onChange: (value: string) => void = () => undefined;
  private onTouched: () => void = () => undefined;

  public ngAfterViewInit(): void {
    if (!isPlatformBrowser(this.platformId) || !this.editorHost) {
      return;
    }

    this.editorView = new EditorView({
      state: EditorState.create({
        doc: this.value,
        extensions: [
          lineNumbers(),
          highlightActiveLineGutter(),
          highlightActiveLine(),
          drawSelection(),
          history(),
          markdown(),
          syntaxHighlighting(defaultHighlightStyle, { fallback: true }),
          EditorView.lineWrapping,
          keymap.of([
            indentWithTab,
            ...defaultKeymap,
            ...historyKeymap
          ]),
          this.editableCompartment.of(EditorView.editable.of(!this.disabled)),
          this.placeholderCompartment.of(placeholder(this.placeholderText)),
          EditorView.updateListener.of((update) => {
            if (!update.docChanged) {
              return;
            }

            this.value = update.state.doc.toString();
            this.onChange(this.value);
          }),
          EditorView.domEventHandlers({
            blur: () => {
              this.onTouched();
            }
          }),
          EditorView.theme({
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

  public ngOnDestroy(): void {
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

    if (!this.editorView) {
      return;
    }

    this.editorView.dispatch({
      effects: this.editableCompartment.reconfigure(EditorView.editable.of(!isDisabled))
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
      selection: EditorSelection.range(from + urlStart, from + replacement.length - 1),
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
      selection: EditorSelection.range(selectionStart, selectionEnd),
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
      selection: EditorSelection.range(startLine.from, startLine.from + replacement.length),
      scrollIntoView: true
    });
    view.focus();
  }
}
