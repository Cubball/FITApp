export interface IPublicationShortInfo {
  id: string;
  type: string;
  name: string;
  mainAuthor: IMainAuthor;
  dateOfPublication: string;
}

export interface IPublicationsPagedList {
  page: number;
  pageSize: number;
  total: number;
  publications: IPublicationShortInfo[];
}

export interface IPublication {
  id: string;
  name: string;
  type: string;
  pagesCount: number;
  pagesByAuthorCount: number;
  annotation: string;
  eVersionLink: string;
  dateOfPublication: string;
  inputData: string;
  authors: IAuthor[];
}

export interface ICreateUpdatePublication {
  name: string;
  type: string;
  pagesCount: number;
  pagesByAuthorCount: number;
  annotation: string;
  eVersionLink: string;
  dateOfPublication: string;
  inputData: string;
  authors: ICreateUpdatePublicationAuthor[];
}

export interface IAuthor {
  id?: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  pagesByAuthorCount: number;
}

export interface ICreateUpdatePublicationAuthor {
  id?: string;
  firstName: string;
  lastName: string;
  patronymic: string;
  pagesByAuthorCount?: number;
}

export interface IMainAuthor {
  firstName: string;
  lastName: string;
  patronymic: string;
}
