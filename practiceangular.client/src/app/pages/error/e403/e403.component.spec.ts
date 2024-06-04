import { ComponentFixture, TestBed } from '@angular/core/testing';

import { E403Component } from './e403.component';

describe('E403Component', () => {
  let component: E403Component;
  let fixture: ComponentFixture<E403Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [E403Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(E403Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
