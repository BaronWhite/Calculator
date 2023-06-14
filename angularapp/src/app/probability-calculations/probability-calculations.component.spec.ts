import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ProbabilityCalculationType } from '../interfaces/probability-calculation-type';
import { ProbabilityCalculationsComponent } from './probability-calculations.component';
import { ProbabilityCalculationsService } from './probability-calculations.service';

describe('ProbabilityCalculationsComponent', () => {
  let component: ProbabilityCalculationsComponent;
  let fixture: ComponentFixture<ProbabilityCalculationsComponent>;
  let probabilityCalculationsService: jasmine.SpyObj<ProbabilityCalculationsService> =
    jasmine.createSpyObj('ProbabilityCalculationsService', [
      'getSupportedCalculations', 'calculate']);
  let calcTypes: ProbabilityCalculationType[] = [
    {
      display: 'Multiply',
      description: '(A)(B)',
      calculationType: 0,
    },
    {
      display: 'Divide',
      description: '(A)/(B)',
      calculationType: 1,
    }
  ];
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProbabilityCalculationsComponent],
      imports: [
        MatAutocompleteModule,
        MatInputModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        NoopAnimationsModule,
      ],
      providers: [
        { provide: ProbabilityCalculationsService, useValue: probabilityCalculationsService }
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ProbabilityCalculationsComponent);
    component = fixture.componentInstance;
    probabilityCalculationsService.getSupportedCalculations.and.resolveTo(calcTypes);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load supported calculation types', () => {
    expect(component.options).toEqual(calcTypes);
  });

  describe('filter', () => {
    it('should not filter if empty', () => {
      component.calculationTypeControl.setValue('');
      component.filteredOptions.subscribe(value =>
        expect(value).toEqual(calcTypes));
    });

    // tech debt
    xit('should filter values based on name', () => {
      component.calculationTypeControl.setValue('Div');
      component.filteredOptions.subscribe(value =>
        expect(value).toEqual([calcTypes[1]]));
    });

    xit('should filter values based on object', () => {
      component.calculationTypeControl.setValue(calcTypes[0]);
      component.filteredOptions.subscribe(value =>
        expect(value).toEqual([calcTypes[0]]));
    });
  });

  describe('displayFn', () => {
    it('should return type display', () => {
      let expected = calcTypes[0].display;

      let actual = component.displayFn(calcTypes[0]);

      expect(actual).toEqual(expected);
    });
  });

  describe('setCalculationType', () => {
    it('should set type on form group', () => {
      let expected = calcTypes[1].calculationType;

      component.setCalculationType(calcTypes[1])
      let actual = component.calculationFormGroup.controls['calculationType'].value;

      expect(actual).toEqual(expected);
    });
  });

  describe('calculate', () => {
    beforeEach(() => {
      probabilityCalculationsService.calculate.and.resolveTo((0.125));
      probabilityCalculationsService.calculate.calls.reset();
    });

    it('should call service with form value if valid', () => {
      component.calculationFormGroup.patchValue({
        a: 0.5,
        b: 0.75,
        calculationType: 5,
      });

      component.calculate();

      expect(probabilityCalculationsService.calculate).toHaveBeenCalledWith(component.calculationFormGroup.value);
    });

    it('should not call service if invalid', () => {
      component.calculationFormGroup.patchValue({
        a: 0.5,
        b: 1,
        calculationType: undefined,
      });

      component.calculate();

      expect(probabilityCalculationsService.calculate).not.toHaveBeenCalled();
    });
  });
});
