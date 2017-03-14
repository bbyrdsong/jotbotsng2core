import { ApiDataService } from './../../services/ApiDataService';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'code-article',
    templateUrl: './code-article.component.html'
})
export class CodeArticlesComponent implements OnInit {
    constructor(private api: ApiDataService) { 
        api.setApiController('CodeArticles');
    }

    ngOnInit() { }
}