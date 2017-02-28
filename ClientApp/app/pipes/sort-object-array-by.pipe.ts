import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'sortObjArrBy' })
export class SortObjArrByPipe implements PipeTransform {
    transform(arr: any[], ...args: string[]) {
        if (!args[0] || args[0].length === 0 || !arr) {
            return arr;
        }

        let prop = args[0];

        return arr.sort((a, b) => {
            if (a[prop] < b[prop]) {
                return -1;
            }
            if (a[prop] > b[prop]) {
                return 1;
            }
            return 0;
        });
    }
}
