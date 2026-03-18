import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { marked } from 'marked';
import DOMPurify from 'dompurify';

@Component({
  selector: 'rr-markdown-viewer',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="rr-markdown" [innerHTML]="sanitizedHtml"></div>
  `,
  styles: [
    `
    :host {
      display: block;
      --rr-markdown-block-background: color-mix(in srgb, var(--p-content-background) 72%, var(--p-form-field-background) 28%);
      --rr-markdown-block-radius: var(--p-form-field-border-radius);
    }
    .rr-markdown { height: 100%; overflow: auto; }
    .rr-markdown h1, .rr-markdown h2, .rr-markdown h3 { margin: 0.5rem 0; }
    .rr-markdown p { margin: 0.25rem 0; }
    .rr-markdown pre { background: var(--rr-markdown-block-background); padding: 0.5rem; border-radius: var(--rr-markdown-block-radius); }
    .rr-markdown code { background: var(--rr-markdown-block-background); padding: 0 0.25rem; border-radius: var(--rr-markdown-block-radius); }
    .rr-markdown ul, .rr-markdown ol { padding-left: 1.25rem; }
    `
  ]
})
export class MarkdownViewerComponent implements OnChanges {
  @Input() markdown: string | null | undefined;
  sanitizedHtml: SafeHtml = '';

  constructor(private sanitizer: DomSanitizer) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['markdown']) {
      const raw = this.markdown ?? '';
      const html = marked.parse(raw);
      const clean = DOMPurify.sanitize(html as string);
      this.sanitizedHtml = this.sanitizer.bypassSecurityTrustHtml(clean);
    }
  }
}

