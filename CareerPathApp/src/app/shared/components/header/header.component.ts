import { Component } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { NgIf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';



//  If it tells them, “You’re 3 Udemy courses away from being PM-ready,” you’re golden.
//  If it tells them, “Here’s a bunch of affiliate links,” they’ll bounce.
@Component({
  selector: 'app-header',
  imports: [ MatMenuModule, MatIconModule, MatButtonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isHomePage: boolean = false;
  imgSource: string = "assets/CareerPathLogoLight.svg";

  constructor(private router: Router) {}

  ngOnInit(): void {
    // Check the current route on init
    this.checkIfHomePage();
    
    // Optionally, subscribe to route changes
    this.router.events.subscribe(() => {
      this.checkIfHomePage();
    });
  }
   checkIfHomePage(): void {
    const currentUrl = this.router.url;
    this.isHomePage = currentUrl === '/';
    this.imgSource = this.isHomePage ? "assets/CareerPathLogoLight.svg" : "assets/CareerPathLogo.svg";
  }
}
