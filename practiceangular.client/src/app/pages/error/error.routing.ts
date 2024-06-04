import { Routes } from '@angular/router';
import { E403Component } from './e403/e403.component';

export const ErrorRoutes: Routes = [
    {
        path: '',
        children: [
            {
                path: '403',
                component: E403Component,
            },
        ],
    },
];
  