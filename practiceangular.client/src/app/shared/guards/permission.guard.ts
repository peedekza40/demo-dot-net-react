import { Injectable, inject } from '@angular/core';
import { CanMatch, Router, Route, UrlTree } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable({
    providedIn: 'root'
})
export class PermissionGuard implements CanMatch {
    constructor(private accountService: AccountService, private router: Router) {}

    canMatch(route: Route): boolean {
        return this.hasPermission(route);
    }
    
    private hasPermission(route: Route) {
        if (this.accountService.isHavePermission(route.path ?? "/")) {
            return true;
        } 
        else {
            this.router.navigate(['/auth/login']);
            return false;
        }
    }
}