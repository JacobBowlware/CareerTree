// app.component.ts
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { _CareerPathService } from './core/services/careerPath.service';
import { HttpClient } from '@angular/common/http';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';

@Component({
  standalone: true,
  providers: [HttpClient],
  imports: [RouterModule, HeaderComponent, FooterComponent], // Required for <router-outlet>
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
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