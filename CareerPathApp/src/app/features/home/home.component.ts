import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [MatIconModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  loading: boolean = false;
  dragOver: boolean = false;

  constructor(private router: Router) {}

  handleGetStarted(): void {
    this.router.navigate(['/signup']);
  }


  handleFileUpload(file: any): void {
      this.loading = true;

      // Upload file to backend
      console.log("File uploaded:", file);
  }

  onDragOver(evt: DragEvent) {
    evt.preventDefault();
    this.dragOver = true;
  }

  onDragLeave() {
    this.dragOver = false;
  }

  onDrop(evt: DragEvent) {
    evt.preventDefault();
    this.dragOver = false;
    if (evt.dataTransfer?.files.length) {
      this.handleFileUpload(evt.dataTransfer.files[0]);
    }
  }
}
