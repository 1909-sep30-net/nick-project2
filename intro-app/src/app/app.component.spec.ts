import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { Component } from '@angular/core';
import { AuthService } from './auth.service';

describe('AppComponent', () => {
  beforeEach(async(() => {
    const authSpy = jasmine.createSpyObj('AuthService',
      ['localAuthSetup', 'handleAuthCallback']);

    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        RouterOutletStubComponent,
        NavbarStubComponent
      ],
      providers: [
        { provide: AuthService, useValue: authSpy }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'intro-app'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('intro-app');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('.content span').textContent).toContain('the fridge is running!');
  });
});

// we need something to take the place of the router outlet directive...
// and this works fine

// alternative: use NO_ERRORS_SCHEMA option
// on the testingmodule above, that will tell it
// to ignore every element it doesn't recognize

// tslint:disable-next-line: component-selector
@Component({ selector: 'router-outlet', template: '' })
class RouterOutletStubComponent { }

@Component({ selector: 'app-navbar', template: '' })
class NavbarStubComponent { }
