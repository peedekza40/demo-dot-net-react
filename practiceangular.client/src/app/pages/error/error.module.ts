import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatIconModule } from '@angular/material/icon';

import { ErrorRoutes } from './error.routing';

import { E403Component } from './e403/e403.component';


@NgModule({
  imports: [
    CommonModule,
    MatIconModule,
    RouterModule.forChild(ErrorRoutes),
  ],
  declarations: [
    E403Component
  ],
})
export class ErrorModule { }
