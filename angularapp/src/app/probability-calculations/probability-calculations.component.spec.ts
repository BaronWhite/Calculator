import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProbabilityCalculationsComponent } from './probability-calculations.component';

describe('ProbabilityCalculationsComponent', () => {
  let component: ProbabilityCalculationsComponent;
  let fixture: ComponentFixture<ProbabilityCalculationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProbabilityCalculationsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProbabilityCalculationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
