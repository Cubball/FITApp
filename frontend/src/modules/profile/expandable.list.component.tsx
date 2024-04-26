import { Children, useState } from 'react';
import PlusIcon from '../../assets/icons/plus-icon.svg';
import ExpandableListItem from './expandable.list.item.component';

const ExpandableList = ({ title, icon, children, onAddClick, onDeleteClick }) => {
  const childrenArray = Children.toArray(children);
  const canExpand = childrenArray.length > 1;
  const [expanded, setExpanded] = useState(false);
  return (
    <div className="mx-1 my-8 rounded-md p-5 shadow shadow-gray-400">
      <div className="flex items-center justify-between">
        <h2 className="text-lg font-semibold">
          {title} <img src={icon} className="inline pl-1" />
        </h2>
        <button onClick={onAddClick}>
          <img src={PlusIcon} />
        </button>
      </div>
      {canExpand && expanded ? (
        childrenArray.map((child, index) => (
          <ExpandableListItem
            element={child}
            canExpand={canExpand}
            index={index}
            key={index}
            onDeleteClick={onDeleteClick}
          />
        ))
      ) : childrenArray.length > 0 ? (
        <ExpandableListItem
          element={childrenArray[0]}
          canExpand={canExpand}
          index={0}
          onDeleteClick={onDeleteClick}
        />
      ) : null}
      {canExpand ? (
        <>
          <button
            className="text-sm w-full pt-3 text-center font-semibold"
            onClick={() => setExpanded((previous) => !previous)}
          >
            {expanded ? 'Приховати' : 'Показати все'}
          </button>
        </>
      ) : null}
    </div>
  );
};

export default ExpandableList;
