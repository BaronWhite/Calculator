import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { ProbabilityCalculation } from '../interfaces/probability-calculation';
import { ProbabilityCalculationType } from '../interfaces/probability-calculation-type';
import { ProbabilityCalculationsService } from './probability-calculations.service';

@Component({
  selector: 'app-probability-calculations',
  templateUrl: './probability-calculations.component.html',
  styleUrls: ['./probability-calculations.component.css']
})
export class ProbabilityCalculationsComponent {
  options: ProbabilityCalculationType[] = [];
  filteredOptions!: Observable<ProbabilityCalculationType[]>;

  private readonly parameterValidators = [Validators.required, Validators.min(0), Validators.max(1)];
  calculationTypeControl = new FormControl<string | ProbabilityCalculationType>('');
  calculationFormGroup: FormGroup = new FormGroup({
    calculationType: new FormControl<undefined | number>(undefined, Validators.required),
    a: new FormControl<number>(0, this.parameterValidators),
    b: new FormControl<number>(0, this.parameterValidators),
  });
  result!: number;

  constructor(private calculationsService: ProbabilityCalculationsService) { }

  async ngOnInit() {
    this.options = await this.calculationsService.getSupportedCalculations();
    this.filteredOptions = this.calculationTypeControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.display;
        return name ? this._filter(name as string) : this.options.slice();
      }),
    );
  }

  displayFn(calculationType: ProbabilityCalculationType): string {
    return calculationType && calculationType.display ? calculationType.display : '';
  }

  private _filter(value: string): ProbabilityCalculationType[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option => option.display.toLowerCase().includes(filterValue));
  }

  setCalculationType(calcType: ProbabilityCalculationType) {
    this.calculationFormGroup.patchValue({
      calculationType: calcType.calculationType,
    });
  }

  async calculate() {
    if (!this.calculationFormGroup.valid) return;
    let calculation: ProbabilityCalculation = this.calculationFormGroup.value;
    this.result = await this.calculationsService.calculate(calculation)
  }
}
