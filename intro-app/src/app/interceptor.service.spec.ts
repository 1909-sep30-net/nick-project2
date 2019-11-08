import { TestBed } from '@angular/core/testing';

import { InterceptorService } from './interceptor.service';
import { AuthService } from './auth.service';

describe('InterceptorService', () => {
  const authSpy = jasmine.createSpyObj('AuthService', ['getTokenSilently$']);

  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      { provide: AuthService, useValue: authSpy }
    ]
  }));

  it('should be created', () => {
    const service: InterceptorService = TestBed.get(InterceptorService);
    expect(service).toBeTruthy();
  });
});
