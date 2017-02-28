import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';

@Injectable()
export class ApiDataService {
    private baseUrl: string;
    private apiController: string;
    private options: RequestOptions;

    constructor(private http: Http) { 
        this.baseUrl = 'api';
        let headers = new Headers({ 'Content-Type': 'application/json' });
        this.options = new RequestOptions({ headers: headers });
    }

    setApiController(apiController: string): void {
        this.apiController = apiController;
    }

    apiControllerIsSet(): boolean {
        return this.apiController && this.apiController !== '';
    }

    get(id: number): Observable<any> {
        return this.http.get(`${this.url()}/${id}`, this.options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getAll(): Observable<any> {
        return this.http.get(`${this.url()}`, this.options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    post(obj: any): Observable<any> {
        return this.http.post(`${this.url()}`, JSON.stringify(obj), this.options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    put(obj: any): Observable<any> {
        return this.http.put(`${this.url()}`, JSON.stringify(obj), this.options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    remove(id: number): Observable<any> {
        return this.http.delete(`${this.url()}/${id}`, this.options)
    }

    private url(): string {
        return `${this.baseUrl}/${this.apiController}`;
    }

    private extractData(res: Response) {
        let body = res.json();
        return body.data || body;
    }
    private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}