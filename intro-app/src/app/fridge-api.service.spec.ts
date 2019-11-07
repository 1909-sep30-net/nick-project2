import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { FridgeApiService } from './fridge-api.service';

describe('FridgeApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientTestingModule ]
  }));

  it('should be created', () => {
    const service: FridgeApiService = TestBed.get(FridgeApiService);
    expect(service).toBeTruthy();
  });
});
