import TrashIcon from '../../assets/icons/trash-icon.svg';

const ExpandableListItem = ({ element, index, canExpand, onDeleteClick }) => {
  return (
    <>
      <div className="flex items-center justify-between p-3">
        {element}
        <button onClick={() => onDeleteClick(index)}>
          <img src={TrashIcon} />
        </button>
      </div>
      {canExpand ? <hr /> : null}
    </>
  );
};

export default ExpandableListItem;
