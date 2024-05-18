export interface IPublicationShortInfo {
  id: string;
  type: string; // TODO: maybe enum
  name: string;
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
  coauthors: ICoauthor[];
}

export interface ICreateUpdatePublication {
  name: string;
  type: string;
  pagesCount: number;
  pagesByAuthorCount: number;
  annotation: string;
  eVersionLink: string;
  dateOfPublication: string;
  coauthors: ICoauthor[];
}

export interface IAuthor {
  id: string;
  firstName: string;
  lastName: string;
  patronymic: string;
}

export interface ICoauthor {
  id?: string;
  firstName: string;
  lastName: string;
  patronymic: string;
}
