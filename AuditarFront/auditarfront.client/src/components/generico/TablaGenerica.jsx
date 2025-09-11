import React, { useState, useRef } from "react";

const PAGE_SIZE = 10;

export default function TablaGenerica({
  columns = [],
  data = [],
  acciones = null,
  pageSize = PAGE_SIZE,
  className = "",
}) {
  const [currentPage, setCurrentPage] = useState(1);
  const [colWidths, setColWidths] = useState(columns.map(() => 150)); // ancho inicial

  const startX = useRef(null);
  const startWidth = useRef(null);
  const resizingCol = useRef(null);

  const handleMouseDown = (e, idx) => {
    startX.current = e.clientX;
    startWidth.current = colWidths[idx];
    resizingCol.current = idx;
    document.addEventListener("mousemove", handleMouseMove);
    document.addEventListener("mouseup", handleMouseUp);
  };

  const handleMouseMove = (e) => {
    if (resizingCol.current === null) return;
    const diff = e.clientX - startX.current;
    setColWidths((widths) =>
      widths.map((w, i) =>
        i === resizingCol.current ? Math.max(60, startWidth.current + diff) : w
      )
    );
  };

  const handleMouseUp = () => {
    resizingCol.current = null;
    document.removeEventListener("mousemove", handleMouseMove);
    document.removeEventListener("mouseup", handleMouseUp);
  };

  const totalPages = Math.ceil(data.length / pageSize);
  const paginatedData = data.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  return (
    <div className={`bg-white shadow-xl rounded-4 overflow-x-auto border border-gray-100 ${className}`}>
      <table className="table table-hover align-middle table-bordered mb-0 rounded-4 overflow-hidden">
        <thead className="tabla-app-thead">
          <tr>
            {columns.map((col, idx) => (
              <th
                key={col.key}
                style={{
                  ...col.style,
                  width: colWidths[idx],
                  minWidth: 60,
                  position: "relative",
                  userSelect: "none",
                }}
              >
                <div style={{ display: "flex", alignItems: "center" }}>
                  <span
                    style={{
                      flex: 1,
                      whiteSpace: "nowrap",
                      overflow: "hidden",
                      textOverflow: "ellipsis",
                    }}
                    title={col.header}
                  >
                    {col.header}
                  </span>
                  <span
                    style={{
                      cursor: "col-resize",
                      padding: "0 4px",
                      userSelect: "none",
                    }}
                    onMouseDown={(e) => handleMouseDown(e, idx)}
                  >
                    <svg width="8" height="24">
                      <rect x="3" y="4" width="2" height="16" fill="#bbb" />
                    </svg>
                  </span>
                </div>
              </th>
            ))}
            {acciones && <th className="text-center">Acciones</th>}
          </tr>
        </thead>
        <tbody>
          {paginatedData.map((row, idx) => (
            <tr key={row.id || idx}>
              {columns.map((col, cidx) => (
                <td key={col.key} style={{ width: colWidths[cidx], minWidth: 60 }}>
                  {col.render ? col.render(row) : row[col.key]}
                </td>
              ))}
              {acciones && <td className="text-center">{acciones(row)}</td>}
            </tr>
          ))}
        </tbody>
      </table>
      {/* Paginación */}
      <div className="d-flex justify-content-between align-items-center mt-3 px-3 pb-2">
        <span>
          Mostrando {data.length === 0 ? 0 : (currentPage - 1) * pageSize + 1}
          &nbsp;-&nbsp;
          {Math.min(currentPage * pageSize, data.length)}
          &nbsp;de&nbsp;
          {data.length} registros
        </span>
        <nav>
          <ul className="pagination mb-0">
            <li className={`page-item ${currentPage === 1 ? "disabled" : ""}`}>
              <button className="page-link" onClick={() => setCurrentPage(currentPage - 1)}>&laquo;</button>
            </li>
            {[...Array(totalPages)].map((_, idx) => (
              <li key={idx} className={`page-item ${currentPage === idx + 1 ? "active" : ""}`}>
                <button className="page-link" onClick={() => setCurrentPage(idx + 1)}>{idx + 1}</button>
              </li>
            ))}
            <li className={`page-item ${currentPage === totalPages ? "disabled" : ""}`}>
              <button className="page-link" onClick={() => setCurrentPage(currentPage + 1)}>&raquo;</button>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  );
}