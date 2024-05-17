import { NavLink, useLocation } from 'react-router-dom';
import { usePublicationsList } from '../../shared/hooks/publications-list.hook';
import Loading from '../../shared/components/loading';
import PlusIcon from '../../assets/icons/plus-icon.svg'
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
    return <Loading />
  }

  if (!publicationsList) {
    return <Error />
  }

  const totalPages = Math.ceil(publicationsList.total / publicationsList.pageSize);
  return (
    <div className="flex h-full flex-col items-center justify-between overflow-y-auto px-10 py-5">
      <div className="w-full">
        <div className='flex justify-between'>
          <h1 className="py-3 text-xl font-bold">Список публікацій</h1>
          <NavLink to="/publications/new" className="border border-main-text rounded-full p-3 h-fit font-semibold flex items-center">
            <img src={PlusIcon} className='max-h-[20px] mr-1 aspect-square inline' />
            <span>
              Додати публікацію
            </span>
          </NavLink>
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
