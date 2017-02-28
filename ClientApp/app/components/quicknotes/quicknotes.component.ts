import { Observable } from 'rxjs';
import { ApiDataService } from './../../services/ApiDataService';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'quick-notes',
    templateUrl: './quicknotes.component.html',
    styleUrls: ['./quicknotes.component.css']
})
export class QuickNotesComponent implements OnInit {
    dbSet: any[];
    model: any;
    filter: string;
    showForm: boolean;

    constructor(private api: ApiDataService) {
        api.setApiController("quicknotes");
    }

    ngOnInit() {
        this.model = {};
        this.get();
    }

    get(): void {
        this.api.getAll().subscribe(
            data => this.dbSet = data.items,
            err => console.log(err)
        );
    }

    save(form: FormGroup, event: Event): void {
        if (event) {
            event.preventDefault();
        }

        if (this.model.id) {
            this.api.put(this.model)
                .subscribe(
                    data => this.model = data,
                    err => console.log(err)
                );
        } else {
            this.api.post(this.model)
                .subscribe(
                    data => this.model = data,
                    err => console.log(err)
                );
        }

        this.showForm = false;
    }

    add(): void {
        this.model = {};
        this.showForm = true;
    }

    edit(model: any, event: Event): void {
        if (event) {
            event.preventDefault();
        }
        this.model = model;
        this.showForm = true;
    }

    delete(model: any, event: Event): void {
        if (event) {
            event.preventDefault();
        }

        this.api.remove(model);
    }
}