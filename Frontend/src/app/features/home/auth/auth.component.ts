import { SHARED_MODULES } from './../../../shared/modules/shared.moudle';
import { Component, SimpleChanges } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { log } from 'console';
import { filter, Subject, take, takeUntil } from 'rxjs';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent {
  isSignIn: boolean = true;
  private destroy$ = new Subject<void>();
  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.setIsExistingUser();
    this.router.events
      .pipe(
        takeUntil(this.destroy$),
        filter((event) => event instanceof NavigationEnd)
      )
      .subscribe(() => {
        this.setIsExistingUser();
      });
  }

  private setIsExistingUser(): void {
    this.route.firstChild?.data
      .pipe(takeUntil(this.destroy$), take(1))
      .subscribe((data) => {
        console.log(data);
        this.isSignIn = data['isSignIn'];
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
