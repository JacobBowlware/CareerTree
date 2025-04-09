import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [MatIconModule, CommonModule, HttpClientModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  loading: boolean = false;
  dragOver: boolean = false;

  constructor(private router: Router, private http: HttpClient) {}

  handleGetStarted(): void {
    this.router.navigate(['/signup']);
  }


  handleFileUpload(file: any): void {
    this.loading = true;

    const formData = new FormData();
    formData.append('resume', file);
  
    this.http.post('/api/resume/upload', formData).subscribe({
      next: (res) => {
        console.log('Upload successful:', res);
        // redirect to either skill assessment or signup page
      },
      error: (err) => {
        console.error('Upload failed:', err);
        // show error UI
      },
      complete: () => {
        this.loading = false;
      }
    });
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
