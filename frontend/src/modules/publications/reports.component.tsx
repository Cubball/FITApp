import { NavLink } from 'react-router-dom';
import { useState } from 'react';
import { useReport } from '../../shared/hooks/report.hook';
import { Field, Form, Formik } from 'formik';

const Reports = () => {
  const [report, setReport] = useState<string | null>();
  const { generateReport, isGenerateReportLoading } = useReport(setReport);

  return (
    <div className="flex flex-col items-center gap-5 p-5">
      <h1 className="border-b border-gray-300 px-10 pb-1 text-center text-xl font-semibold">
        Згенерувати звіт
      </h1>
      {report ? (
        <>
          <embed
            src={report}
            type="application/pdf"
            className="aspect-square w-full max-w-xl rounded-md md:w-1/2"
          ></embed>
          <a
            href={report}
            download="Звіт.pdf"
            className="w-full max-w-xl rounded-md bg-main-text p-2 text-center text-white disabled:bg-gray-400 md:w-1/2"
          >
            Завантажити
          </a>
          <button
            className="w-full max-w-xl rounded-md border border-main-text p-2 text-center md:w-1/2"
            onClick={() => setReport(null)}
          >
            Скасувати
          </button>
        </>
      ) : (
        <Formik
          initialValues={{
            startDate: '',
            endDate: ''
          }}
          onSubmit={generateReport}
        >
          <Form className="flex w-full flex-col items-center gap-5">
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="dateOfPublication" className="block">
                Починаючи з:
              </label>
              <Field
                id="startDate"
                name="startDate"
                type="date"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <div className="w-full max-w-xl md:w-1/2">
              <label htmlFor="dateOfPublication" className="block">
                До:
              </label>
              <Field
                id="endDate"
                name="endDate"
                type="date"
                required
                className="w-full rounded-md border border-gray-300 p-2"
              />
            </div>
            <button
              className="w-full max-w-xl rounded-md bg-main-text p-2 text-white disabled:bg-gray-400 md:w-1/2"
              disabled={isGenerateReportLoading}
            >
              Згенерувати
            </button>
            <NavLink
              className="w-full max-w-xl rounded-md border border-main-text p-2 text-center md:w-1/2"
              to="/publications"
            >
              Скасувати
            </NavLink>
          </Form>
        </Formik>
      )}
    </div>
  );
};

export default Reports;
