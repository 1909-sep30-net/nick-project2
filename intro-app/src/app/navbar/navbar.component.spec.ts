import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarComponent } from './navbar.component';
import { AuthService } from '../auth.service';
import { FridgeApiService } from '../fridge-api.service';

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;

  beforeEach(async(() => {
    const authSpy = jasmine.createSpyObj('AuthService', ['login', 'logout']);
    const fridgeSpy = jasmine.createSpy('FridgeApiService');

    authSpy.loggedIn = false;

    TestBed.configureTestingModule({
      declarations: [NavbarComponent],
      providers: [
        { provide: AuthService, useValue: authSpy },
        { provide: FridgeApiService, useValue: fridgeSpy },
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
