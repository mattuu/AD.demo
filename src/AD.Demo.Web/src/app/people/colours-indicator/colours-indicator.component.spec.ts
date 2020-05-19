import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColoursIndicatorComponent } from './colours-indicator.component';

describe('ColoursIndicatorComponent', () => {
  let component: ColoursIndicatorComponent;
  let fixture: ComponentFixture<ColoursIndicatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColoursIndicatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColoursIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
