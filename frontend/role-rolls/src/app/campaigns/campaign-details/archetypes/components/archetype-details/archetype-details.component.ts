import { Component } from '@angular/core';
import { Editor } from 'primeng/editor';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'rr-archetype-details',
  imports: [
    Editor,
    FormsModule
  ],
  templateUrl: './archetype-details.component.html',
  styleUrl: './archetype-details.component.scss'
})
export class ArchetypeDetailsComponent {
  public details: string;

}
