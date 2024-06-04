import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import LoginForm from '../models/LoginForm';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient){}

  public login(data: LoginForm): void {
    
  }

  public logout(): void {

  }

  public register(): void {
    
  }

  public isHavePermission(path: string): boolean {
    return true;
  }
}
