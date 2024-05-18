import { NavLink, useLocation } from 'react-router-dom';
import { usePublicationsList } from '../../shared/hooks/publications-list.hook';
import Loading from '../../shared/components/loading';
import PlusIcon from '../../assets/icons/plus-icon.svg';
import ReportIcon from '../../assets/icons/report-icon.svg';
import Error from '../../shared/components/error';
import PublicationEntry from './publication-entry.component';
import Pagination from '../../shared/components/pagination';

const PublicationsList = () => {
  const location = useLocation();
  const page = new URLSearchParams(location.search).get('page');
  const { publicationsList, isLoading, deletePublicationById } = usePublicationsList(
    Number(page ?? 1)
  );
  if (isLoading) {
    return <Loading />;
  }

  if (!publicationsList) {
    return <Error />;
  }

  const totalPages = Math.ceil(publicationsList.total / publicationsList.pageSize);
  return (
    <div className="flex h-full flex-col items-center justify-between overflow-y-auto px-10 py-5">
      <div className="w-full">
        <div className="flex items-center justify-between">
          <h1 className="py-3 text-xl font-bold">Список публікацій</h1>
          <div className="flex flex-col gap-3 md:flex-row">
            <NavLink
              to="/reports"
              className="flex h-fit items-center rounded-full border border-main-text p-3 font-semibold"
            >
              <img src={ReportIcon} className="mr-1 inline aspect-square max-h-[20px]" />
              <span>Звіти</span>
            </NavLink>
            <NavLink
              to="/publications/new"
              className="flex h-fit items-center rounded-full border border-main-text p-3 font-semibold"
            >
              <img src={PlusIcon} className="mr-1 inline aspect-square max-h-[20px]" />
              <span>Додати публікацію</span>
            </NavLink>
          </div>
        </div>
        {publicationsList.publications.map((publication) => (
          <PublicationEntry
            key={publication.id}
            publication={publication}
            onDelete={deletePublicationById}
          />
        ))}
      </div>
      <Pagination link="/publications" totalPages={totalPages} page={publicationsList.page} />
    </div>
  );
};

export default PublicationsList;
