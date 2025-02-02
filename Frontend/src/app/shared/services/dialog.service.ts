import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ConfirmDialogComponent } from '../components/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  constructor(private dialog: MatDialog) {}

  /** Confirmation dialog with Yes/No buttons */
  confirm(message: string): Observable<boolean> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: { message, isConfirmation: true },
      disableClose: true, // Prevent closing on outside click
    });

    return dialogRef.afterClosed();
  }

  /** Message dialog with only an OK button */
  showMessage(message: string): Observable<void> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: { message, isConfirmation: false },
      disableClose: true,
    });
    return dialogRef.afterClosed();
  }
}
