import { NavLink, useParams } from 'react-router-dom';
import { usePublication } from '../../shared/hooks/publication.hook';
import Loading from '../../shared/components/loading';
import Error from '../../shared/components/error';
import { useEffect, useState } from 'react';
import { ICoauthor } from '../../services/publications/publications.types';
import AddCoauthorModal from './add-coauthor-modal.component';
import { Field, Form, Formik } from 'formik';
import XIcon from '../../assets/icons/x-icon.svg';

const AddUpdatePublication = () => {
  const [addCoauthorModalOpen, setAddCoauthorModalOpen] = useState(false);
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
  const [coauthors, setCoauthors] = useState<ICoauthor[]>([]);
  useEffect(() => {
    if (publication) {
      setCoauthors(publication.coauthors);
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
      <AddCoauthorModal
        isOpen={addCoauthorModalOpen}
        onClose={() => setAddCoauthorModalOpen(false)}
        employees={employees}
        onAdd={(coauthor) => {
          if (!coauthor.id || !coauthors.find((a) => a.id === coauthor.id)) {
            setCoauthors([...coauthors, coauthor]);
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
            annotation: publication?.annotation ?? '',
            eVersionLink: publication?.eVersionLink ?? '',
            dateOfPublication: publication?.dateOfPublication ?? ''
          }}
          onSubmit={(values) => {
            if (publication) {
              updatePublication({
                id: publication.id,
                publication: {
                  ...values,
                  coauthors
                }
              });
            } else {
              createPublication({
                ...values,
                coauthors
              });
            }
          }}
        >
          <Form className="flex w-full flex-col items-center gap-5">
            <Field
              id="name"
              name="name"
              placeholder="Введіть назву"
              required
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
            />
            <Field
              id="type"
              name="type"
              as="select"
              required
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
            >
              <option value="" disabled hidden>
                Оберіть тип
              </option>
              <option value="Підручник">Підручник</option>
              <option value="Навчальні посібники">Навчальні посібники</option>
              <option value="Методичні рекомендації">Методичні рекомендації</option>
              <option value="Монографія">Монографія</option>
              <option value="Статті">Статті</option>
              <option value="Інше">Інше</option>
            </Field>
            <Field
              id="annotation"
              name="annotation"
              placeholder="Введіть анотацію"
              required
              as="textarea"
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
            />
            <Field
              id="eVersionLink"
              name="eVersionLink"
              placeholder="Введіть посилання на електронну версію"
              required
              className="w-full max-w-xl rounded-md border border-gray-300 p-2 md:w-1/2"
            />
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="dateOfPublication" className="block">
                Введіть дату публікації:
              </label>
              <Field
                id="dateOfPublication"
                name="dateOfPublication"
                type="date"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            {coauthors.length > 0 && (
              <div className="w-full max-w-xl md:w-1/2">
                <div className="w-full">Співавтори:</div>
                {coauthors.map((coauthor, index) => {
                  let displayName = "<ім'я не вказано>";
                  if (coauthor.firstName && coauthor.lastName && coauthor.patronymic) {
                    displayName = `${coauthor.lastName} ${coauthor.firstName} ${coauthor.patronymic}`;
                  }
                  return (
                    <div
                      key={index}
                      className="mt-2 flex w-full justify-between rounded-md border border-gray-300 p-2"
                    >
                      <span>{displayName}</span>
                      <img
                        src={XIcon}
                        className="max-h-[20px] cursor-pointer"
                        onClick={() =>
                          setCoauthors(coauthors.filter((a) => a !== coauthors[index]))
                        }
                      />
                    </div>
                  );
                })}
              </div>
            )}
            <button
              type="button"
              className="w-full max-w-xl rounded-md border border-dashed border-gray-300 p-2 md:w-1/2"
              onClick={() => setAddCoauthorModalOpen(true)}
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
