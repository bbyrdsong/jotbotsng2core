import { ApiDataService } from './../../services/ApiDataService';
import { Component, OnInit } from '@angular/core';
import { CodeArticle } from "./code-article";

@Component({
    selector: 'code-article',
    templateUrl: './code-articles.component.html'
})
export class CodeArticlesComponent implements OnInit {
    items: CodeArticle[];
    article: CodeArticle;

    constructor(private api: ApiDataService) {
        api.setApiController('CodeArticles');
        this.items = [];
    }

    ngOnInit() {
        this.api.getAll().subscribe(
            resp => { 
                this.items = resp.items; 
                document.getElementById('article').innerHTML = this.items[0].description; 
            },
            err => console.log(err)
        );
    }
}