import { NavLink, useParams } from 'react-router-dom';
import { usePublication } from '../../shared/hooks/publication.hook';
import Loading from '../../shared/components/loading';
import Error from '../../shared/components/error';
import { useEffect, useState } from 'react';
import AddAuthorModal from './add-author-modal.component';
import { Field, Form, Formik } from 'formik';
import XIcon from '../../assets/icons/x-icon.svg';
import { ICreateUpdatePublicationAuthor } from '../../services/publications/publications.types';

const AddUpdatePublication = () => {
  const [addAuthorModalOpen, setAddAuthorModalOpen] = useState(false);
  const params = useParams();
  const id = params.publicationId;
  const {
    publication,
    isPublicationLoading,
    employees,
    areEmployeesLoading,
    createPublication,
    isCreatePublicationLoading,
    updatePublication,
    isUpdatePublicationLoading
  } = usePublication(id);
  const [authors, setAuthors] = useState<ICreateUpdatePublicationAuthor[]>([]);
  useEffect(() => {
    if (publication) {
      setAuthors(publication.authors);
    }
  }, [isPublicationLoading]);
  if (isPublicationLoading || areEmployeesLoading) {
    return <Loading />;
  }

  if (!employees) {
    return <Error />;
  }

  return (
    <>
      <AddAuthorModal
        isOpen={addAuthorModalOpen}
        onClose={() => setAddAuthorModalOpen(false)}
        employees={employees}
        onAdd={(author) => {
          if (!author.id || !authors.find((a) => a.id === author.id)) {
            setAuthors([...authors, author]);
          }
        }}
      />
      <div className="flex flex-col items-center gap-5 p-5">
        <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
          {publication ? 'Оновити публікацію' : 'Додати нову публікацію'}
        </h1>
        <Formik
          initialValues={{
            name: publication?.name ?? '',
            type: publication?.type ?? '',
            pagesCount: publication?.pagesCount ?? 0,
            pagesByAuthorCount: publication?.pagesByAuthorCount ?? 0,
            annotation: publication?.annotation ?? '',
            eVersionLink: publication?.eVersionLink ?? '',
            dateOfPublication: publication?.dateOfPublication ?? '',
            inputData: publication?.inputData ?? ''
          }}
          onSubmit={(values) => {
            if (publication) {
              updatePublication({
                id: publication.id,
                publication: {
                  ...values,
                  authors: authors
                }
              });
            } else {
              createPublication({
                ...values,
                authors: authors
              });
            }
          }}
        >
          <Form className="flex w-full flex-col items-center gap-5">
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="name">Назва:</label>
              <Field
                id="name"
                name="name"
                placeholder="Введіть назву"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="type">Тип:</label>
              <Field
                id="type"
                name="type"
                as="select"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              >
                <option value="" disabled hidden>
                  Оберіть тип
                </option>
                <option value="Підручник">Підручник</option>
                <option value="Навчальні посібники">Навчальні посібники</option>
                <option value="Методичні рекомендації">Методичні рекомендації</option>
                <option value="Монографія">Монографія</option>
                <option value="Статті">Статті</option>
                <option value="Авторське право">Авторське право</option>
                <option value="Інше">Інше</option>
              </Field>
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="pagesCount">Кількість сторінок:</label>
              <Field
                id="pagesCount"
                name="pagesCount"
                placeholder="Введіть кількість сторінок в публікації"
                type="number"
                min={0}
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="pagesByAuthorCount">Кількість сторінок, написаних Вами:</label>
              <Field
                id="pagesByAuthorCount"
                name="pagesByAuthorCount"
                placeholder="Введіть кількість сторінок в публікації, написаних Вами"
                type="number"
                min={0}
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="annotation">Анотація:</label>
              <Field
                id="annotation"
                name="annotation"
                placeholder="Введіть анотацію"
                required
                as="textarea"
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="inputData">Вхідні дані:</label>
              <Field
                id="inputData"
                name="inputData"
                placeholder="Введіть вхідні дані"
                required
                as="textarea"
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="eVersionLink">Посилання на електронну версію:</label>
              <Field
                id="eVersionLink"
                name="eVersionLink"
                placeholder="Введіть посилання на електронну версію"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="dateOfPublication" className="block">
                Дата публікації:
              </label>
              <Field
                id="dateOfPublication"
                name="dateOfPublication"
                type="date"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            {authors.length > 0 && (
              <div className="w-full max-w-xl md:w-1/2">
                <div className="w-full">Співавтори:</div>
                {authors.map((author, index) => {
                  let displayName = "<ім'я не вказано>";
                  if (author.firstName && author.lastName && author.patronymic) {
                    displayName = `${author.lastName} ${author.firstName} ${author.patronymic}`;
                  }
                  return (
                    <div
                      key={index}
                      className="mt-2 flex w-full justify-between rounded-md border border-gray-300 p-2"
                    >
                      <span>{displayName}{author.pagesByAuthorCount && ` - ${author.pagesByAuthorCount} с.`}</span>
                      <img
                      src={XIcon}
                      className="max-h-[20px] cursor-pointer"
                      onClick={() => setAuthors(authors.filter((a) => a !== authors[index]))}
                      />
                    </div>
                  );
                })}
              </div>
            )}
            <button
              type="button"
              className="w-full max-w-xl rounded-md border border-dashed border-gray-300 p-2 md:w-1/2"
              onClick={() => setAddAuthorModalOpen(true)}
            >
              Додати співавтора
            </button>
            <button
              className="w-full max-w-xl rounded-md bg-main-text p-2 text-white disabled:bg-gray-400 md:w-1/2"
              type="submit"
              disabled={isCreatePublicationLoading || isUpdatePublicationLoading}
            >
              Зберегти
            </button>
            <NavLink
              className="w-full max-w-xl rounded-md border border-main-text p-2 text-center md:w-1/2"
              to="/publications"
            >
              Скасувати
            </NavLink>
          </Form>
        </Formik>
      </div>
    </>
  );
};

export default AddUpdatePublication;
