import { SHARED_MODULES } from './../../shared/modules/shared.moudle';
import { Component, SimpleChanges } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent {
  isExistingUser:boolean = true;
  constructor(private route: ActivatedRoute){
  }
  ngOnInit(): void {
  this.setIsExistingUser();
  }
  ngOnChanges(changes: SimpleChanges): void {
    // Watch for changes and update `isExistingUser` accordingly
    if (changes['route']) {
      this.setIsExistingUser();
    }
  }
  private setIsExistingUser(): void {
    this.route.data.subscribe(data => {
      debugger;
      this.isExistingUser = data['isExistingUser'];
      console.log('isExistingUser in AuthComponent:', this.isExistingUser); // To debug
    });
  }
}
