import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FridgeComponent } from './fridge.component';
import { FridgeApiService } from '../fridge-api.service';
import { ReactiveFormsModule } from '@angular/forms';

describe('FridgeComponent', () => {
  let component: FridgeComponent;
  let fixture: ComponentFixture<FridgeComponent>;

  beforeEach(async(() => {
    const apiSpy = jasmine.createSpyObj('FridgeApiService', ['getItems']);
    // the spy service will say there are no items in the fridge
    apiSpy.getItems.and.returnValue(Promise.resolve([]));

    TestBed.configureTestingModule({
      declarations: [FridgeComponent],
      imports: [ReactiveFormsModule],
      providers: [
        { provide: FridgeApiService, useValue: apiSpy }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FridgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have the open image when open', () => {
    component.open = true;
    // the name of the image contains "open"
    expect(component.imageUrl).toContain('open');
  });
});
