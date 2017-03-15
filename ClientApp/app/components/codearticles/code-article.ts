export class CodeArticle {
    id: number;
    name: string;
    description: string;
    tags: string;
    createdBy: string;
    createdDate: Date;
    modifiedBy: string;
    modifiedDate: Date;

    constructor(name: string, description: string,
        tags: string) {
            this.name = name;
            this.description = description;
            this.tags = tags;
        }
}