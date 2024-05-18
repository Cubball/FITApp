import { useMutation } from 'react-query';
import { reportService } from '../../services/publications/report.service';
import { addInfoToast, addSuccessToast, createOnError } from '../helpers/toast.helpers';

interface IUseReportHookReturn {
  generateReport: (request: { startDate: string; endDate: string }) => void;
  isGenerateReportLoading: boolean;
}

export const useReport = (onReportGenerated: (blobUrl: string) => void): IUseReportHookReturn => {
  const { mutateAsync: generateReport, isLoading: isGenerateReportLoading } = useMutation({
    mutationFn: ({ startDate, endDate }: { startDate: string; endDate: string }) =>
      reportService.getReport(startDate, endDate),
    onSuccess: (blob) => {
      onReportGenerated(blob);
      addSuccessToast('Звіт згенеровано');
    },
    onMutate: () => addInfoToast('Звіт генерується...'),
    onError: createOnError('Не вдалося згенерувати звіт')
  });

  return { generateReport, isGenerateReportLoading };
};
