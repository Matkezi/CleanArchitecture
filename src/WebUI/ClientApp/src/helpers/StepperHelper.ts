export class StepperHelper {

    static getStep(): number {
        const url = window.location.href;
        const step = url.split('step=')[1];
        return +step;
    }

    static increasedStepUrl(): string {
        return `step=${this.getStep() + 1}`;
    }

    static decreasedStepUrl(): string {
        return `step=${this.getStep() - 1}`;
    }
    
    static stepUrl(step: number): string {
        return `step=${step}`
    }
}