import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { FridgeApiService } from './fridge-api.service';
import { AuthService } from './auth.service';
import { empty } from 'rxjs';

describe('FridgeApiService', () => {
  const authSpy = jasmine.createSpyObj('AuthService', ['login', 'logout']);
  authSpy.userProfile$ = empty();

  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientTestingModule],
    providers: [
      { provide: AuthService, useValue: authSpy }
    ]
  }));

  it('should be created', () => {
    const service: FridgeApiService = TestBed.get(FridgeApiService);
    expect(service).toBeTruthy();
  });
});
