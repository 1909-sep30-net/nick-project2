import { TestBed } from '@angular/core/testing';

import { FridgeApiService } from './fridge-api.service';

describe('FridgeApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FridgeApiService = TestBed.get(FridgeApiService);
    expect(service).toBeTruthy();
  });
});
