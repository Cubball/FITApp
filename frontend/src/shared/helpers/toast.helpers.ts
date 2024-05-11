import { toast } from 'react-toastify';

export const createOnError = (text: string) => () => {
  toast(text, {
    type: 'error'
  });
};

export const addSuccessToast = (text: string) => {
  toast(text, {
    type: 'success'
  });
};
