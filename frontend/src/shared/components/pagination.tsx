import { NavLink } from 'react-router-dom';

const MAX_PAGES_AROUND = 3;

const Pagination = ({
  page,
  totalPages,
  link
}: {
  page: number;
  totalPages: number;
  link: string;
}) => {
  if (!page || page > totalPages) {
    page = 1;
  }

  const renderEllipsisBack = page - 1 > MAX_PAGES_AROUND;
  const renderEllipsisForward = totalPages - page > MAX_PAGES_AROUND;
  const rangeStart = Math.max(1, page - MAX_PAGES_AROUND + (renderEllipsisBack ? 1 : 0));
  const rangeEnd = Math.min(totalPages, page + MAX_PAGES_AROUND - (renderEllipsisForward ? 1 : 0));
  const pagesInMiddle: number[] = [];
  for (let i = rangeStart; i <= rangeEnd; i++) {
    pagesInMiddle.push(i);
  }

  return (
    <div className="max-w-fit rounded-xl bg-main-background p-2 font-semibold *:mx-1">
      {page > 1 ? <NavLink to={`${link}?page=${page - 1}`}>Назад</NavLink> : null}
      {renderEllipsisBack ? (
        <>
          <NavLink to={`${link}?page=1`} className="px-2">
            1
          </NavLink>
          <span>...</span>
        </>
      ) : null}
      {pagesInMiddle.map((num) => (
        <NavLink
          to={`${link}?page=${num}`}
          key={num}
          className={num == page ? 'rounded-md bg-main-text px-2 py-1 text-white' : 'px-2 py-1'}
        >
          {num}
        </NavLink>
      ))}
      {renderEllipsisForward ? (
        <>
          <span>...</span>
          <NavLink to={`${link}?page=${totalPages}`} className="px-2">
            {totalPages}
          </NavLink>
        </>
      ) : null}
      {page < totalPages ? <NavLink to={`${link}?page=${page + 1}`}>Вперед</NavLink> : null}
    </div>
  );
};

export default Pagination;
