import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MarkdownEditorComponent } from '@app/shared/components/markdown-editor/markdown-editor.component';

@Component({
  selector: 'rr-archetype-details',
  imports: [
    FormsModule,
    MarkdownEditorComponent
  ],
  templateUrl: './archetype-details.component.html',
  styleUrl: './archetype-details.component.scss'
})
export class ArchetypeDetailsComponent {
  public details: string;

}
