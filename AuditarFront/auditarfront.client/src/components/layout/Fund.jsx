const Fund = () => {
    return (
        <div
            className="position-absolute w-100 text-center user-select-none"
            style={{
                top: 0,
                left: 0,
                zIndex: 0,
                height: '100%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                pointerEvents: 'none',
            }}
        >
            <span
                style={{
                    fontSize: '10vw',
                    fontWeight: 900,
                    color: '#2563eb',
                    opacity: 0.10,
                    letterSpacing: '0.2em',
                    textShadow: '0 2px 16px #1e40af44',
                }}
            >
                INSPECTIA-WEB
            </span>
        </div>
    );
};

export default Fund;
