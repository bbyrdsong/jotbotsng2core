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
        this.api.getAll().subscribe(
            data => this.dbSet = data.items,
            err => console.log(err)
        );
    }
}