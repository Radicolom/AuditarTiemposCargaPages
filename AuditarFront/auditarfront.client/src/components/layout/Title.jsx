const Title = ({ icon, title, subtitle, actions }) => {

    return (
        <div className="rounded-4 mb-4 p-4 d-flex flex-column flex-md-row align-items-center justify-content-between gap-3"
            style={{
                background: 'linear-gradient(90deg, #2563eb 0%, #1e40af 100%)',
                boxShadow: '0 4px 24px 0 rgba(30,64,175,0.10)',
            }}>
            <div className="d-flex flex-column flex-md-row align-items-center gap-3">
                <div className="d-flex align-items-center justify-content-center rounded-circle bg-white bg-opacity-25" style={{width: 56, height: 56}}>
                    {icon}
                </div>
                <div>
                    <h1 className="mb-1 fw-bold text-white" style={{fontSize: '2.2rem', letterSpacing: '-1px'}}>
                        {title}
                    </h1>
                    <p className="mb-0 text-white-50" style={{fontWeight: 400}}>
                        {subtitle}
                    </p>
                </div>
            </div>
            <div className="d-flex gap-2">
                {actions.map((btn, idx) => (
                <button
                    key={idx}
                    type="button"
                    className="btn btn-light d-flex align-items-center px-4 py-2 fw-bold shadow-sm"
                    style={{ fontSize: '1.1rem' }}
                    onClick={btn.onClick}
                >
                    {btn.icon}
                    {btn.text}
                </button>
                ))}
            </div>
        </div>
    );
};

export default Title;
