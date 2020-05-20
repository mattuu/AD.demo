import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColourIconComponent } from './colour-icon.component';

describe('ColourIconComponent', () => {
  let component: ColourIconComponent;
  let fixture: ComponentFixture<ColourIconComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColourIconComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColourIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
