
import './css/RunningText.css';

function RunningText({
    text,                 
    speed = 15,           
    separator = '•',
    background = '#DF301C',
    color = '#fff',
}) {
    if (!text || (Array.isArray(text) && text.length === 0)) return null;

    const messages = Array.isArray(text) ? text : [text];
    const content = messages.join(` ${separator} `) + ` ${separator} `;

    return (
        <div className="marquee" style={{ background, color }}>
            <div className="marquee-track" style={{ animationDuration: `${speed}s` }}>
                <span>{content}</span>
                <span aria-hidden="true">{content}</span>
            </div>
        </div>
    );
}

export default RunningText;