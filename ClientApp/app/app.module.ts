import { CodeArticlesComponent } from './components/codearticles/code-articles.component';
import { FilterPipe } from './pipes/filter.pipe';
import { SortObjArrByPipe } from './pipes/sort-object-array-by.pipe';
import { ApiDataService } from './services/ApiDataService';
import { QuickNotesComponent } from './components/quicknotes/quicknotes.component';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        QuickNotesComponent,
        CodeArticlesComponent,
        SortObjArrByPipe,
        FilterPipe
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'quick-notes', component: QuickNotesComponent },
            { path: 'code-articles', component: CodeArticlesComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        FormsModule
    ],
    providers: [ApiDataService],
})
export class AppModule {
}

