import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [MatIconModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  
  counter(n: number): any[] {
    return new Array(n).fill(1);
  }
}
