// app.component.ts
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { _CareerPathService } from './core/services/careerPath.service';
import { HttpClient } from '@angular/common/http';

@Component({
  standalone: true,
  providers: [HttpClient],
  imports: [RouterModule], // Required for <router-outlet>
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  backendResponse = '';
  _CareerPathService: _CareerPathService;

  constructor(_CareerPathService: _CareerPathService) {
    this._CareerPathService = _CareerPathService;
  }

  testBackend() {
    this._CareerPathService.testConnection().subscribe((response: any) => {
      this.backendResponse = response.message;
    });
  }
}