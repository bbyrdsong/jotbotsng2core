import { ApiDataService } from './../../services/ApiDataService';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
    selector: 'quick-note-details',
    templateUrl: './quicknotes-details.component.html'
})
export class QuickNoteDetailsComponent implements OnInit {
    qnForm: FormGroup;

    constructor(private fb: FormBuilder, private api: ApiDataService) {
        this.createForm();
    }

    ngOnInit(): void {

    }

    createForm(): void {
        this.qnForm = this.fb.group({
            name: '',
            description: ''
        });
    }
}