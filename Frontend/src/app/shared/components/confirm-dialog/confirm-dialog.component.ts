import { SHARED_MODULES } from './../../modules/shared.moudle';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.scss',
})
export class ConfirmDialogComponent {
  isConfirmation: boolean = true; // Default to confirmation dialog

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: { message: string; isConfirmation?: boolean }
  ) {
    this.isConfirmation = data.isConfirmation ?? true;
  }

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onClose(): void {
    this.dialogRef.close();
  }
}
