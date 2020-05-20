import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ColourStatsComponent } from './colour-stats.component';

describe('ColourStatsComponent', () => {
  let component: ColourStatsComponent;
  let fixture: ComponentFixture<ColourStatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ColourStatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ColourStatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
